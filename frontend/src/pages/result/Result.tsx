import React from "react";
import { Plane } from "../quiz/Quiz";

interface ResultProps {
    plane: Plane;
}

export default class Result extends React.Component<ResultProps> {
    constructor(props: ResultProps) {
        super(props);
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
