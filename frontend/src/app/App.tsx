import React from 'react';
import { BrowserRouter, Link } from 'react-router-dom';
import { Route, Router, Routes } from 'react-router';
import Landing from '../pages/landing/Landing';
import { Plane, WrappedQuiz } from '../pages/quiz/Quiz';
import Result from '../pages/result/Result';
import './App.css';
import { NoPlane } from '../pages/errors/noPlane/NoPlane';
import { NotFound } from '../pages/errors/notFound/NotFound';
import { Analytics } from '../pages/analytics/Analytics';
import { WrappedSignIn } from '../pages/signIn/SignIn';

interface AppState {
  plane?: Plane
}

export default class App extends React.Component<{}, AppState> {
  constructor() {
    // TODO: fix warning
    super({});

    this.state = {};
  }

  setPlane(plane: Plane) {
    this.setState({
      plane: plane
    });
  }

  render(): React.ReactNode {
    return (
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Landing />} />
          <Route path='quiz' element={<WrappedQuiz finishCallback={this.setPlane.bind(this)}/>} />
          <Route path='result' element={this.state.plane ? <Result plane={this.state.plane}/> : <NoPlane />} />
          <Route path='analytics' element={<Analytics />} />
          <Route path='signIn' element={<WrappedSignIn />} />
          <Route path='*' element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    );

    /*
    const window = this.state.windowState;

    switch (window) {
      case WindowState.Landing:
        return <Landing clickHandler={this.landingClickHandler.bind(this)}/>;
      case WindowState.Quiz:
        return <Quiz finishCallback={this.calculateResult.bind(this)}/>;
      case WindowState.Result:
        return this.state.plane ? <Result plane={this.state.plane}/> : <p>Plane is null</p>;
    } 
    */
  }
}
