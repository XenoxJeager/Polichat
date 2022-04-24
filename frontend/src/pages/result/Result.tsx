import axios from "axios";
import React from "react";
import { getUrl } from "../../config/Constants";
import { Plane } from "../quiz/Quiz";


interface ResultProps {
    plane: Plane;
}



export default class Result extends React.Component<ResultProps> {
    constructor(props: ResultProps) {
        super(props);
        
    }

    componentDidMount() {    
        axios.post("http://localhost:3001/evaluation",)
        .then((response) => {
           const returnedData = response.data;
           this.setState({
               ideologies : returnedData,
           })
        })
        .catch((reason) => {});
    }


    render(): React.ReactNode {
        const ideology = this.state.ideologies.text;
        const description = this.state.ideologies.text;
        return (
            <div>
                <h1>Your Result is: {this.props.plane.x.toFixed(3)} | {this.props.plane.y.toFixed(3)}</h1>
                <p>{ideology}</p>
                <p>{description}</p>
            </div>
        );
    }
}
