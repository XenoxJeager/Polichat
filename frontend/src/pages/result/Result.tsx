import axios from "axios";
import React, { useEffect, useRef } from 'react';
import { getUrl } from "../../config/Constants";
import { Vector } from "../quiz/Quiz";
import { Chat } from "./components/Chat";
import chad from "./imgs/chad.png";
import img from "./imgs/political_compass_str.png"
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


    circleDraw = () => {
        // return (
        //   <Stage width={window.innerWidth} height={window.innerHeight}>
            
        //   </Stage>
        // );
      };

    renderResult(): React.ReactNode {
        const name = this.state.ideology!.name;
        const description = this.state.ideology!.description;
        

        
        return (
                <div>
                    <div className="grid grid-cols-2 divide-x borderWidth:0 pt-5">
                        <div className="place-items-center h-screen ">
                            <div className="grid place-items-center v-screen mx-14 my-5">
                                <h1 className="text-lg">Your Result is: {this.props.vector.x.toFixed(3)} | {this.props.vector.y.toFixed(3)}</h1>
                                <h1 className="text-4xl bold mb-5">{name}</h1>

                                <img className="w-3/4" src={img} alt="political"></img>
                                <h1 className="italic">{description}</h1>                        
                            </div>
                        </div>
                        <div className="mx-14 my-5 text-center border-4 bg-gray-100">
                            <h1 className="text-3xl bold border-x-4 border-b-4 p-5">Chat with your comrades!</h1>
                            <div className="p-5 border-x-4">
                                <div className="h-[80vh] overflow-y-scroll flex flex-col-reverse  "
                                    style={{ backgroundImage:`url(${chad})`, backgroundRepeat:'no-repeat', backgroundPosition:'fixed', backgroundSize:'98%'}}>
                                    <div className="">
                                    <Chat vector={this.props.vector}/>
                                    </div> 
                                </div>
                            </div>
                        </div>
                    </div>
                    {/* IMO Footer doesn't look good on the Result Page */}
                </div>
        );
    }
}
