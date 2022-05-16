export abstract class ChatMessage {
    private static runninId: number = 0;

    readonly id: number;
    readonly text: string;

    constructor(text: string) {
        this.id = ChatMessage.runninId++;
        this.text = text;
    }

    abstract render(): React.ReactNode;
}

export class AdminChatMessage extends ChatMessage {
    render(): React.ReactNode {
        return <li className="font-bold italic text-xl" key={this.id}>{this.text}</li>;
    }
}

export class LocalChatMessage extends ChatMessage {
    render(): React.ReactNode {
        return <li className="text-right text-xl font-bold text-indigo-800" key={this.id}>{this.text}</li>;
    }
}

export class RemoteChatMessage extends ChatMessage {
    render(): React.ReactNode {
        return <li className="text-left text-xl font-bold text-red-600" key={this.id}>{this.text}</li>;
    }
}
