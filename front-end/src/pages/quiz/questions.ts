export interface Weight {
    weightX: number;
    weightY: number;
}

export interface WeightedQuestion {
   weights: Weight[];
   text: string;
}

export interface SelectableWeightedQuestion extends WeightedQuestion {
    selectedWeight: Weight;
}

export function normalizeQuestions(questions: WeightedQuestion[]): SelectableWeightedQuestion[] {
    // returns the most extreme positive value in a weighted question
    const getNum = (question: WeightedQuestion, mult: number, weight: keyof Weight): number => {
        const resTop = question.weights[0][weight] * mult;
        const resBot = question.weights[3][weight] * mult;
        return Math.max(resTop, resBot); 
    }

    // function to add all values
    const sumVals = (mult: number, weight: keyof Weight): number => {
        let sum = 0;

        questions.forEach(question => { 
            sum += getNum(question, mult, weight);
        });

        return sum;
    }

    // get the sums for all four directions
    let sumPosX: number = sumVals(1, "weightX");
    let sumNegX: number = sumVals(-1, "weightX");
    let sumPosY: number = sumVals(1, "weightY");
    let sumNegY: number = sumVals(-1, "weightY");

    // generate new questions
    let idx = 0;
    let newQuestions: SelectableWeightedQuestion[] = Array(questions.length);
    questions.forEach(question => {
        let qWeights: Weight[] = question.weights;
        let weights: Weight[] = Array(4);

        // generate the new weights
        for (let i = 0; i <= 3; i++) {
            let x = qWeights[i].weightX;
            let y = qWeights[i].weightY;

            // divide the original weights by the sum corresponding if it is either positive of negative
            weights[i] = {
                weightX: x / (x > 0 ? sumPosX : sumNegX),
                weightY: y / (y > 0 ? sumPosY : sumNegY)
            }
        }

        newQuestions[idx] = {
            weights: weights,
            text: question.text,
            selectedWeight: {weightX: 0, weightY: 0}
        };
        idx++;
    });

    return newQuestions;
}
