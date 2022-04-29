import React from "react";
import { getUrl } from "../../../config/Constants";
import { Vector } from "../../quiz/Quiz";
import { AdminChatMessage, ChatMessage, RemoteChatMessage } from "./ChatMessage";

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
        const roomId = (this.props.vector.x > 0 ? 1 : 0) + (this.props.vector.y > 0 ? 2 : 0);
        const connStr = "ws://localhost:3001/ws?room_id=" + roomId;
        this.ws = new WebSocket(connStr);

        this.ws.onopen = () => {
            this.setState({
                chatHistory: this.state.chatHistory.concat(
                    new AdminChatMessage("Connected to Chat")
                )
            });
        };

        this.ws.onmessage = (ev: MessageEvent<any>) => {
            this.setState({
                chatHistory: this.state.chatHistory.concat(
                    new RemoteChatMessage(ev.data as string)
                )
            });
        };
        
        this.setState({
            chatHistory: this.state.chatHistory.concat(
                new AdminChatMessage("Connecting...")
            ),
            state: WindowState.Active
        })
    }

    renderInactive(): React.ReactNode {
        return <button onClick={this.connect.bind(this)}>Connect!</button>;
    }

    renderActive(): React.ReactNode {
        let chatHistory: React.ReactNode[] = this.state.chatHistory
        .map((message: ChatMessage, _: number) => message.render());

        return (
            <>
                <ol>
                    {chatHistory}
                </ol>
                <input></input>
                <button>Send</button>
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
            <div className="float-right">
                {content}
            </div>
        );
    }
}
