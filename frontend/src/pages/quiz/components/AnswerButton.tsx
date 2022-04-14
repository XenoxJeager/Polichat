import React from "react";
import { Weight } from "../questions";

interface AnswerButtonProps {
    text: string,
    weight: Weight,
    onClick: (weight: Weight) => void;
}

// ? color buttons corresponding to where you would move ?
export const AnswerButton: React.FC<AnswerButtonProps> = ({text, weight, onClick}) => (
    <button onClick={() => onClick(weight)}>
        {text} {(weight.weightX).toFixed(3)} | {(weight.weightY).toFixed(3)}
    </button>
);
