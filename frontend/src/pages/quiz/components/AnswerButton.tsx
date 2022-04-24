import React from "react";
import { Vector } from "../Quiz";

interface AnswerButtonProps {
    text: string;
    vector: Vector;
    onClick: (vector: Vector) => void;
    styling: string;
}

// ? color buttons corresponding to where you would move ?
export const AnswerButton: React.FC<AnswerButtonProps> = ({text, vector, onClick, styling}) => (
    <div className={styling + " inline-block px-8 py-4 duration-150 ease-in-out text-white font-medium text-s leading-tight uppercase transition"}>
        <button onClick={() => onClick(vector)} className="inline-block">
            <b>{text}</b>
        </button>
    </div>
);
