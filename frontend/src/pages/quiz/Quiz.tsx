import axios from "axios";
import React from "react";
import { NavigateFunction, useNavigate } from "react-router";
import { AnswerButton } from "./components/AnswerButton";
import { normalizeQuestions, SelectableWeightedQuestion, Weight, WeightedQuestion } from "./questions";

interface QuizProps {
    finishCallback: (plane: Plane) => void;
    navigate?: NavigateFunction;
}

enum WindowState {
    Loading,
    Active
}

interface QuizState {
    windowState: WindowState;
    index: number;
    normalizedQuestions?: SelectableWeightedQuestion[] | undefined;
}

export interface Plane {
    x: number;
    y: number;
}

export class Quiz extends React.Component<QuizProps, QuizState> {
    plane: Plane = { x: 0, y: 0};

    constructor(props: QuizProps) {
        super(props);
        this.state = {
            windowState: WindowState.Loading,
            index: 0
        }

        this.updatePlane = this.updatePlane.bind(this);
    }

    componentDidMount() {    
        axios.get("http://localhost:3001/questions")
            .then((response) => {
                const questions = response.data as WeightedQuestion[];
                this.setState({
                    windowState: WindowState.Active,
                    normalizedQuestions: normalizeQuestions(questions)
                });
            })
            .catch((reason) => {});
    }

    updatePlane(weight: Weight) {
        const func = (weight: Weight, mult: number) => {
            this.plane.x += weight.weightX * mult;
            this.plane.y += weight.weightY * mult;
        };

        const currentQuestion = this.state.normalizedQuestions?.[this.state.index];

        if (currentQuestion == null) return;

        func(currentQuestion.selectedWeight, -1);
        func(weight, 1);
        currentQuestion.selectedWeight = weight;

        this.tryNextQuestion();
    }

    tryNextQuestion() {
        const nextIndex = this.state.index + 1;

        if(this.state.normalizedQuestions == null) return;

        if (nextIndex >= this.state.normalizedQuestions.length) {
            // we have reached the end of the questions
            this.props.finishCallback(this.plane);

            this.props.navigate!("/result");
        } else {
            // we go to the next question
            this.setState({index: nextIndex});
        }
    }



    tryPreviousQuestion() {
        const nextIndex = this.state.index - 1;

        if (nextIndex < 0) {
            // we have reached the beginning of the questions
            this.props.navigate!("/");
        } else {
            // we go to the previous question
            this.setState({index: nextIndex});
        }
    }

    renderQuiz(): React.ReactNode {
        const currentQuestion = this.state.normalizedQuestions?.[this.state.index];

        if(currentQuestion == null) return;

        const text = currentQuestion.text;

        return (
            <div className="flex flex-col items-center justify-center h-screen">
                <div>
                    <div className="mb-3 text-center">
                        <h1 className="text-2xl "><b>{text}</b></h1>
                    </div>
                    
                    <div className="lg:flex items-center justify-center lg:p-4 grid  v-screen" > 
                        <AnswerButton text="Strongly Agree" styling="lg:rounded-l bg-green-600 hover:bg-green-500 px-10" weight={currentQuestion.weights[0]} onClick={this.updatePlane} />
                        <AnswerButton text="Agree" styling="bg-green-500 hover:bg-green-400 px-11" weight={currentQuestion.weights[1]} onClick={this.updatePlane} />
                        <AnswerButton text="Neutral/Skip" styling="text-black bg-white-500 hover:bg-gray-100" weight={{weightX: 0, weightY: 0}} onClick={this.updatePlane}/>
                        <AnswerButton text="Disagree" styling="bg-red-500 hover:bg-red-400" weight={currentQuestion.weights[2]} onClick={this.updatePlane} />
                        <AnswerButton text="Strongly Disagree" styling="lg:rounded-r  bg-red-600 hover:bg-red-500" weight={currentQuestion.weights[3]} onClick={this.updatePlane} />
                    </div>

                </div>

                <div className="flex justify-center items-center lg:m-0 m-2.5">
                    <button onClick={this.tryPreviousQuestion.bind(this)} className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-3 px-6 rounded-l">Prev</button>
                    <button onClick={this.tryNextQuestion.bind(this)} className="bg-blue-500 hover:bg-blue-600 text-white font-bold py-3 px-6 rounded-r">Next</button>
                </div>
            </div>
        );
    }

    renderLoading(): React.ReactNode {
        return (
            <div className="flex items-center justify-center h-screen">
                <h1 className="font-bold">Loading...</h1>
            </div>
        );
    }

    render(): React.ReactNode {
        const state = this.state.windowState;

        switch (state) {
            case WindowState.Loading:
                return this.renderLoading();
            case WindowState.Active:
                return this.renderQuiz();
        }
    }
}

export const WrappedQuiz = (props: QuizProps) => {
    return <Quiz {...props} navigate={useNavigate()}/>
};
