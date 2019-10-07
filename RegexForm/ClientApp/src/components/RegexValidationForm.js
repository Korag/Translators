import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, FormText, FormFeedback, Container, Row, Col } from 'reactstrap';
import { NotificationManager } from 'react-notifications';

import '../css/Style.css';

const buttonStyle = {
    margin: 'auto'
}

export class RegexValidationForm extends Component {
    static displayName = RegexValidationForm.name;

    constructor(props) {
        super(props);

        this.state = {
            email: "",
            password: "",
            firstName: "",
            lastName: "",
            date: "",

            emailValidationError: null,
            passwordValidationError: null,
            dateValidationError: null,
            firstNameValidationError: null,
            lastNameValidationError: null,

            emailIsValid: false,
            passwordIsValid: false,
            firstNameIsValid: false,
            lastNameIsValid: false,
            dateIsValid: false,

        }
    }

    changeValue(e) {
        this.setState({
            [e.target.name]: e.target.value
        });
    }

    validateEmail(e) {
        let validEmail = this.state.email.match(/^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)
        if (validEmail == null) {
            this.setState({
                emailIsValid: false,
                emailValidationError: "Wprowadzono adres email w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                emailIsValid: true,
                emailValidationError: null
            });
        }
    }

    validateFirstName(e) {
        let validFirstName = this.state.firstName.match()
        if (validFirstName == null) {
            this.setState({
                firstNameIsValid: false,
                firstNameValidationError: "Wprowadzono imię w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                firstNameIsValid: true,
                firstNameValidationError: null
            });
        }
    }

    validateLastName(e) {
        let validLastName = this.state.lastName.match()
        if (validLastName == null) {
            this.setState({
                lastNameIsValid: false,
                lastNameValidationError: "Wprowadzono nazwisko w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                lastNameIsValid: true,
                lastNameValidationError: null
            });
        }
    }

    validatePassword(e) {
        let validPassword = this.state.password.match()
        if (validPassword == null) {
            this.setState({
                passwordIsValid: false,
                passwordValidationError: "Wprowadzono hasło w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                passwordIsValid: true,
                passwordValidationError: null
            });
        }
    }

    validateDate(e) {
        let validDate = this.state.date.match()
        if (validDate == null) {
            this.setState({
                dateIsValid: false,
                dateValidationError: "Wprowadzono zakres dat w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                dateIsValid: true,
                dateValidationError: null
            });
        }
    }

    onKeyPress(e) {
        if (e.which === 13) {
            e.preventDefault();
        }
    }

    bookSeat = (e) => {
        e.preventDefault();
        if (
            this.state.emailIsValid &&
            this.state.passwordIsValid &&
            this.state.firstNameIsValid &&
            this.state.lastNameIsValid &&
            this.state.dateIsValid) {
            NotificationManager.success('Dodano rezerwację stolika');
        }
        else {
            NotificationManager.error('W formularzu znajdują się niepoprawnie wprowadzone dane');
        }
    }

    render() {
        return (
            <Container>
                <h1 className="header">Rezerwacja stolika</h1>

                <Form onSubmit={e => this.bookSeat(e)} onKeyPress={e => this.onKeyPress(e)} className="form">

                    <FormGroup className="formGroup">
                        <Label for="email">Adres email</Label>
                        <Input name="email" id="emailId" invalid={(!this.state.emailIsValid && this.state.emailValidationError != null) ? true : false} valid={this.state.emailIsValid} onChange={e => this.changeValue(e)} value={this.state.email} onBlur={e => this.validateEmail(e)} />
                        <FormFeedback>{this.state.emailValidationError}</FormFeedback>
                    </FormGroup>

                    <Row form>
                        <Col md={6}>
                            <FormGroup className="formGroup">
                                <Label for="firstName">Imię</Label>
                                <Input type="string" name="firstName" id="firstNameId" invalid={(!this.state.firstNameIsValid && this.state.firstNameValidationError != null) ? true : false} valid={this.state.firstNameIsValid} onChange={e => this.changeValue(e)} value={this.state.firstName} onBlur={e => this.validateFirstName(e)} />
                                <FormFeedback>{this.state.firstNameValidationError}</FormFeedback>
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup className="formGroup">
                                <Label for="lastName">Nazwisko</Label>
                                <Input type="string" name="lastName" id="lastNameId" invalid={(!this.state.lastNameIsValid && this.state.lastNameValidationError != null) ? true : false} valid={this.state.lastNameIsValid} onChange={e => this.changeValue(e)} value={this.state.lastName} onBlur={e => this.validateLastName(e)} />
                                <FormFeedback>{this.state.lastNameValidationError}</FormFeedback>
                            </FormGroup>
                        </Col>
                    </Row>

                    <FormGroup className="formGroup">
                        <Label for="date">Data rezerwacji</Label>
                        <Input type="string" name="date" id="dateId" invalid={(!this.state.dateIsValid && this.state.dateValidationError != null) ? true : false} valid={this.state.dateIsValid} onChange={e => this.changeValue(e)} onChange={e => this.changeValue(e)} value={this.state.date} onBlur={e => this.validateDate(e)} />
                        <FormFeedback>{this.state.dateValidationError}</FormFeedback>
                    </FormGroup>

                    <Row form>
                        <Col sm="12" md={{ size: 6, offset: 3 }}>
                            <FormGroup className="formGroup">
                                <Label for="password">Hasło</Label>
                                <Input type="password" name="password" id="passwordId" invalid={(!this.state.passwordIsValid && this.state.passwordValidationError != null) ? true : false} valid={this.state.passwordIsValid} onChange={e => this.changeValue(e)} value={this.state.password} onBlur={e => this.validatePassword(e)} />
                                <FormFeedback>{this.state.passwordValidationError}</FormFeedback>
                            </FormGroup>
                        </Col>
                    </Row>

                    <div className="row justify-content-center">
                        <Button outline color="primary" type="submit" style={this.buttonStyle}>Rezerwuj</Button>
                    </div>

                </Form>
            </Container>
        );
    }
}


