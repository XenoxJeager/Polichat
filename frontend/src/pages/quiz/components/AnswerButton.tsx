import React from "react";
import { Weight } from "../questions";

interface AnswerButtonProps {
    text: string,
    weight: Weight,
    onClick: (weight: Weight) => void;
}

export const AnswerButton: React.FC<AnswerButtonProps> = ({text, weight, onClick}) => (
    <button onClick={() => onClick(weight)}>
        {text} {(weight.weightX).toFixed(3)} | {(weight.weightY).toFixed(3)}
    </button>
);

interface NeutralButtonProps {
    onClick: () => void;
}

export const NeutralButton: React.FC<NeutralButtonProps> = ({onClick}) => (
    <button onClick={onClick}>Neutral/Skip</button>
);
