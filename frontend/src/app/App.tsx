import React from 'react';
import { BrowserRouter, Link } from 'react-router-dom';
import { Route, Router, Routes } from 'react-router';
import Landing from '../pages/landing/Landing';
import { Vector, WrappedQuiz } from '../pages/quiz/Quiz';
import Result from '../pages/result/Result';
import './App.css';
import { NoPlane } from '../pages/errors/noPlane/NoPlane';
import { NotFound } from '../pages/errors/notFound/NotFound';
import { Analytics } from '../pages/analytics/Analytics';
import { WrappedSignIn } from '../pages/signIn/SignIn';

interface AppState {
  vector?: Vector
}

export default class App extends React.Component<{}, AppState> {
  constructor() {
    // TODO: fix warning
    super({});

    this.state = {};
  }

  setPlane(vector: Vector) {
    this.setState({
      vector: vector
    });
  }

  render(): React.ReactNode {
    return (
      <BrowserRouter>
        <Routes>
          <Route path='/' element={<Landing />} />
          <Route path='quiz' element={<WrappedQuiz finishCallback={this.setPlane.bind(this)}/>} />
          <Route path='result' element={this.state.vector ? <Result vector={this.state.vector}/> : <NoPlane />} />
          <Route path='analytics' element={<Analytics />} />
          <Route path='signIn' element={<WrappedSignIn />} />
          <Route path='*' element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    );
  }
}
