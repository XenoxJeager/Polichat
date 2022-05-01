import axios from "axios";
import React from "react";
import { getUrl } from "../../config/Constants";
import { Vector } from "../quiz/Quiz";
import { Chat } from "./components/Chat";

interface ResultProps {
    vector: Vector;
}

interface IdeologyInfo {
    name: string;
    description: string;
}

enum WindowState {
    Loading,
    Active
}

interface ResultState {
    ideology?: IdeologyInfo;
    state: WindowState;
}

export default class Result extends React.Component<ResultProps, ResultState> {
    constructor(props: ResultProps) {
        super(props);

        this.state = {
            state: WindowState.Loading
        };
    }

    componentDidMount() {    
        axios.get(getUrl(`/evaluation?x=${this.props.vector.x}&y=${this.props.vector.y}`))
        .then((response) => {
           const ideology = response.data as IdeologyInfo;

           this.setState({
               state: WindowState.Active,
               ideology : ideology
           })
        });
    }

    render(): React.ReactNode {
        switch(this.state.state) {
            case (WindowState.Loading):
                return this.renderLoading();
            case (WindowState.Active):
                return this.renderResult(); 
        }
    }

    renderLoading(): React.ReactNode {
        return null;
    }   

    renderResult(): React.ReactNode {
        const name = this.state.ideology!.name;
        const description = this.state.ideology!.description;

        return (
            // result page
            <div className="p-40">
                <h1 className="flex justify-center">Your Result is: {this.props.vector.x.toFixed(3)} | {this.props.vector.y.toFixed(3)}</h1>
                <b className="flex justify-center">{name}</b>
                <p className="flex justify-center text-center">{description}</p>

                <div className="flex flex-row min-h-screen justify-center items-center">
                    <Chat vector={this.props.vector}/>
                </div>

            </div>
        );
    }
}
