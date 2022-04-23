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
        axios.post(getUrl("/evaluation"), 
        {
            x: this.props.plane.x,
            y: this.props.plane.y
        })
        .then((response) => {
            
        });
    }


    render(): React.ReactNode {
        return (
            <div>
                <h1>Your Result is: {this.props.plane.x.toFixed(3)} | {this.props.plane.y.toFixed(3)}</h1>
                <p>Some description</p>
            </div>
        );
    }
}
