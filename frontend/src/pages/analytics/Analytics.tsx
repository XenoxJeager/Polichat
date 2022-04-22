import React from "react";

interface AnalyticsProps {

}

interface Result {
    x: number;
    y: number;
}

interface AnalyticsData {
    results: Result[];
    activeChats: number;
}

interface AnalyticsState {
    data?: AnalyticsData;
}

export class Analytics extends React.Component<AnalyticsProps, AnalyticsState> {
    constructor(props: AnalyticsProps) {
        super(props);
        this.state = {}
    }

    render(): React.ReactNode {
        const loadIfNull = (value?: number): string => {
            return value ? value.toString() : "Loading...";
        };
        const data = this.state.data;

        return (
            <>
                <p>Map</p>
            </>
        );
    }
}
