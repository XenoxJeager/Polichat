import React from "react";
import { Weight } from "../questions";

interface AnswerButtonProps {
    text: string;
    weight: Weight;
    onClick: (weight: Weight) => void;
    styling: string;
}

// ? color buttons corresponding to where you would move ?
export const AnswerButton: React.FC<AnswerButtonProps> = ({text, weight, onClick, styling}) => (
    <div className={styling + " inline-block px-8 py-4 duration-150 ease-in-out text-white font-medium text-s leading-tight uppercase transition"}>
        <button onClick={() => onClick(weight)} className="inline-block">
            <b>{text}</b>
        </button>
    </div>
);
