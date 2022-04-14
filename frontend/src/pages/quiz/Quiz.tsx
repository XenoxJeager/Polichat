import axios from "axios";
import React from "react";
import { NavigateFunction, useNavigate } from "react-router";
import { AnswerButton, NeutralButton } from "./components/AnswerButton";
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

export class Plane {
    x: number = 0;
    y: number = 0;
}

export class Quiz extends React.Component<QuizProps, QuizState> {
    plane: Plane = new Plane();

    constructor(props: QuizProps) {
        super(props);
        this.state = {
            windowState: WindowState.Loading,
            index: 0
        }

        this.updatePlane = this.updatePlane.bind(this);
    }

    componentDidMount() {
        const rndQuestion = (text: string):SelectableWeightedQuestion => {
            let weights: Weight[] = Array(4);
            
            for(let i = 0; i <= 3; i++) {
                let mult = i > 1 ? -1 : 1;
                weights[i] = {
                    weightX: Math.random() * mult,
                    weightY: Math.random() * mult
                }
            }

            return {
                text: text,
                weights: weights,
                selectedWeight: { weightX: 0, weightY: 0}
            };
        }

        let amount = 20;
        let questions: SelectableWeightedQuestion[] = Array(amount);

        for(let i = 0; i < amount; i++)
            questions[i] = rndQuestion("Question " + i)

        this.setState({
            normalizedQuestions: normalizeQuestions(questions),
            windowState: WindowState.Active
        });
        return;
        
        axios.get("localhost:3000/questions")
            .catch((response) => {
                const questions = response.data as WeightedQuestion[];
                this.setState({normalizedQuestions: normalizeQuestions(questions)});
            })
            .then((reason) => {});
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
            (this.props.navigate)!("/result");
        } else {
            // we go to the next question
            this.setState({index: nextIndex});
        }
    }

    tryPreviousQuestion() {
        const nextIndex = this.state.index - 1;

        if (nextIndex < 0) {
            // we have reached the beginning of the questions
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
                    <div className="mb-3">
                        <h1 className="text-2xl">{text}</h1>
                        <p>{this.plane.x.toFixed(3)} | {this.plane.y.toFixed(3)}</p>
                    </div>

                    <div className="justify-center w-full">
                        <div>
                            <AnswerButton text="Strongly Agree" weight={currentQuestion.weights[0]} onClick={this.updatePlane} />
                        </div>

                        <div>
                            <AnswerButton text="Agree" weight={currentQuestion.weights[1]} onClick={this.updatePlane} />
                        </div>

                        <div>
                            <NeutralButton onClick={this.tryNextQuestion.bind(this)} />
                        </div>

                        <div>
                            <AnswerButton text="Disagree" weight={currentQuestion.weights[2]} onClick={this.updatePlane} />
                        </div>
                        
                        <div>
                            <AnswerButton text="Strongly Disagree" weight={currentQuestion.weights[3]} onClick={this.updatePlane} />
                        </div>
                    </div>
                </div>

                <div>
                    <button onClick={this.tryPreviousQuestion.bind(this)}>Previous</button>
                    <button onClick={this.tryNextQuestion.bind(this)}>Next</button>
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
