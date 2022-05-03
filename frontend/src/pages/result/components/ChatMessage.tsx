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
        return <li className="font-bold italic text-lg" key={this.id}>{this.text}</li>;
    }
}

export class LocalChatMessage extends ChatMessage {
    render(): React.ReactNode {
        return <li className="text-right text-lg" key={this.id}>{this.text}</li>;
    }
}

export class RemoteChatMessage extends ChatMessage {
    render(): React.ReactNode {
        return <li className="text-left text-lg" key={this.id}>{this.text}</li>;
    }
}
