import React, { Component } from 'react';
import { Route } from 'react-router';
import { Layout } from './components/Layout';
import { RegexValidationForm } from './components/RegexValidationForm';
import { NotificationContainer } from 'react-notifications';

export default class App extends Component {
    static displayName = App.name;

    render() {
        return (
            <div>
                    <Route exact path='/' component={RegexValidationForm} />

                <div style={{ marginTop: 30 }}>
                    <NotificationContainer />
                </div>
            </div>
        );
    }
}
