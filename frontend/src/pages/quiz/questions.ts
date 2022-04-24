import { Vector } from "./Quiz";

export interface WeightedQuestion {
   weights: Vector[];
   text: string;
}

export interface SelectableWeightedQuestion extends WeightedQuestion {
    selectedWeight: Vector;
}

export function normalizeQuestions(questions: WeightedQuestion[]): SelectableWeightedQuestion[] {
    // returns the most extreme positive value in a weighted question
    const getNum = (question: WeightedQuestion, mult: number, weight: keyof Vector): number => {
        const resTop = question.weights[0][weight] * mult;
        const resBot = question.weights[3][weight] * mult;
        return Math.max(resTop, resBot); 
    }

    // function to add all values
    const sumVals = (mult: number, weight: keyof Vector): number => {
        let sum = 0;

        questions.forEach(question => { 
            sum += getNum(question, mult, weight);
        });

        return sum;
    }

    // get the sums for all four directions
    let sumPosX: number = sumVals(1, "x");
    let sumNegX: number = sumVals(-1, "x");
    let sumPosY: number = sumVals(1, "y");
    let sumNegY: number = sumVals(-1, "y");

    // generate new questions
    let idx = 0;
    let newQuestions: SelectableWeightedQuestion[] = Array(questions.length);
    questions.forEach(question => {
        let qWeights: Vector[] = question.weights;
        let weights: Vector[] = Array(4);

        // generate the new weights
        for (let i = 0; i <= 3; i++) {
            let x = qWeights[i].x;
            let y = qWeights[i].y;

            // divide the original weights by the sum corresponding if it is either positive of negative
            weights[i] = {
                x: x / (x > 0 ? sumPosX : sumNegX),
                y: y / (y > 0 ? sumPosY : sumNegY)
            }
        }

        newQuestions[idx] = {
            weights: weights,
            text: question.text,
            selectedWeight: {x: 0, y: 0}
        };
        idx++;
    });

    return newQuestions;
}
