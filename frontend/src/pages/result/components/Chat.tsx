import React from "react";
import { Vector } from "../../quiz/Quiz";
import { AdminChatMessage, ChatMessage, LocalChatMessage, RemoteChatMessage } from "./ChatMessage";

interface ServerMessage {
    type: string;
    text: string;
}

interface ChatProps {
    vector: Vector;
}

enum WindowState {
    Inactive,
    Active
}

interface ChatState {
    state: WindowState;
    chatHistory: ChatMessage[];
    inputText: string;
}

export class Chat extends React.Component<ChatProps, ChatState> {
    private ws?: WebSocket; 
   
    constructor(props: ChatProps) {
        super(props);

        this.state = {
            state: WindowState.Inactive,
            chatHistory: [],
            inputText: ""
        };
    }

    connect() {
        const roomId = (this.props.vector.x > 0 ? 1 : 0) + (this.props.vector.y < 0 ? 2 : 0);
        const connStr = "ws://localhost:3001/ws?room_id=" + roomId;
        this.ws = new WebSocket(connStr);
        let roomname = ""
        this.ws.onopen = () => {
            

            switch(roomId){
                case(0):
                    roomname = "Authoritarian Left";
                    break;
                case(1):
                    roomname = "Authoritarian Right";
                    break;
                case(2):
                    roomname = "Liberal Left";
                    break
                case(3):
                    roomname = "Liberal Right";
                    break;                  
            }


            this.setState({
                chatHistory: this.state.chatHistory.concat(
                    new AdminChatMessage(`Connected to the ${roomname} Chat`)
                )
            });
        };

        this.ws.onmessage = (ev) => {
            let chatMessage: ChatMessage | undefined;
            let serverMessage = JSON.parse(ev.data) as ServerMessage;

            switch (serverMessage.type) {
                case "local":
                    chatMessage = new LocalChatMessage(serverMessage.text);
                    break;
                case "remote":
                    chatMessage = new RemoteChatMessage(serverMessage.text);
                    break;
                case "admin":
                    chatMessage = new AdminChatMessage(serverMessage.text);
                    break;
            }

            if (chatMessage === undefined)
                return;

            this.setState({
                chatHistory: this.state.chatHistory.concat(chatMessage)
            });
        };
         
        this.setState({
            chatHistory: this.state.chatHistory.concat(
                new AdminChatMessage("Connecting...")
            ),
            state: WindowState.Active
        });
    }

    sendMessage() {
        this.ws?.send(this.state.inputText);
        this.setState({
            inputText: ""
        });
    }

    // TODO: what is the fucking type of ev??????
    handleInput(ev: any) {
        console.log("pressed");
        ev.preventDefault();
        if(ev.key === "Enter") 
            this.sendMessage();
    }

    renderInactive(): React.ReactNode {
        return <button className="text-3xl  " onClick={this.connect.bind(this)}>Connect!</button>;
    }

    renderActive(): React.ReactNode {
        let chatHistory: React.ReactNode[] = this.state.chatHistory
        .map((message: ChatMessage, _: number) => message.render());

        return (
            <>
                <div className="w-4/5">
                    <ol>
                        {chatHistory}
                    </ol>

                    <input 
                        className="text-xl px-4 py-2 mt-2 rounded-5 bg-gray-50 mr-2 mb-3"
                        onChange={(ev) => this.setState({inputText: ev.target.value})} 
                        value={this.state.inputText} 
                        placeholder="Message..."
                        onKeyUp={this.handleInput.bind(this)}>
                    </input>
                    <button className=" bg-blue-500 hover:bg-blue-600 text-white font-bold py-2 px-5 rounded justify-end" onClick={this.sendMessage.bind(this)}>Send</button>
                    
                </div>

            </>
        );
    }

    render(): React.ReactNode {
        let content: React.ReactNode;

        switch(this.state.state) {
            case(WindowState.Inactive):
                content = this.renderInactive();
                break;
            case(WindowState.Active):
                content = this.renderActive();
                break;
        }

        return (
            <div className="flex flex-col items-center ">
                {content}
            </div>
        );
    }
}
