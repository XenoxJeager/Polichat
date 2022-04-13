import React from "react";

interface LandingProps {
    clickHandler: () => void;
}

interface LandingState {
    imageIndex: number;
}

export default class Landing extends React.Component<LandingProps, LandingState> {
    interval: NodeJS.Timer | null;
    images: string[];

    constructor(props: LandingProps) {
        super(props);
        this.state = {
            imageIndex: 0
        }

        this.interval = null;
        this.images = ["iwo-flag.jpg", "berlin-flag.jpg"];
    }

    componentDidMount() {
        this.interval = setInterval(() => this.changeBackgroundImage(), 3000);
    }

    componentWillUnmount() {
        if (this.interval) {
            clearInterval(this.interval);
        }
    }

    changeBackgroundImage() {
        console.log("changing");

        const currentIndex = this.state.imageIndex;
        const noOfImages = this.images.length;
        let newIndex = (currentIndex + 1) % noOfImages;
        this.setState({imageIndex: newIndex});
    }

    render(): React.ReactNode {
        // prevent button from resizing content above
        return (
            <div className="grid place-items-center h-screen">
                <div className="grid place-items-center">
                    <h1 className="text-7xl font-bold">{"Polichat"}</h1>
                    <p className="text-2xl font-bold text-stone-500">Where do <b><i>you</i></b> stand?</p>
                </div>
                
                <div className="flex-none">
                    <button 
                        className="bg-blue-500 text-white border-b-[7px] border-x-[5px] border-blue-700 font-bold py-3.5 px-8 border-blue-700 rounded
                                text-xl
                                hover:bg-blue-500 hover:border-blue-600
                                active:border-b-[4px] active:border-x-[3px]"
                        onClick={this.props.clickHandler}
                    >Take the quiz</button>
                </div>
            </div>
        );
    }
}