import React, { Component } from 'react';
import { Button, Form, FormGroup, Label, Input, Alert, FormFeedback, Container, Row, Col } from 'reactstrap';
import { NotificationManager } from 'react-notifications';

import '../css/Style.css';

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

            emailValidationError: [],
            passwordValidationError: [],
            dateValidationError: [],
            firstNameValidationError: [],
            lastNameValidationError: [],

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
        let validEmail = this.state.email.match(/^(([^<>()\[\]\\.,;:\s@.*"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/)

        if (validEmail == null) {

            this.setState({
                emailIsValid: false,
                emailValidationError: "Wprowadzono adres email w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                emailIsValid: true,
                emailValidationError: []
            });
        }
    }

    validateFirstName(e) {
        let validFirstName = this.state.firstName.match(/^[A-Za-z][A-Za-z\'\-]+([\ A-Za-z][A-Za-z\'\-]+)*$/)
        if (validFirstName == null) {
            this.setState({
                firstNameIsValid: false,
                firstNameValidationError: "Wprowadzono imię w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                firstNameIsValid: true,
                firstNameValidationError: []
            });
        }
    }

    validateLastName(e) {
        let validLastName = this.state.lastName.match(/^[A-Za-z][A-Za-z\'\-]+([\ A-Za-z][A-Za-z\'\-]+)*$/)
        if (validLastName == null) {
            this.setState({
                lastNameIsValid: false,
                lastNameValidationError: "Wprowadzono nazwisko w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                lastNameIsValid: true,
                lastNameValidationError: []
            });
        }
    }

    validatePassword(e) {

        let passwordError = [];

        let validPasswordLength = this.state.password.match(/^.{8,32}$/);

        if (validPasswordLength == null) {
            passwordError.push("Hasło musi mieć długość minimum 8 znaków. \n");
        }

        let validPasswordOneUpperCharacter = this.state.password.match(/(?=.*[A-Z])/)

        if (validPasswordOneUpperCharacter == null) {
            passwordError.push("Hasło musi zawierać co najmniej jedną dużą literę. \n");
        }

        let validPasswordOneDigit = this.state.password.match(/(?=.*\d)/)

        if (validPasswordOneDigit == null) {
            passwordError.push("Hasło musi zawierać co najmniej jedną małą literę. \n");
        }

        let validPasswordOneSpecialCharacter = this.state.password.match(/(?=.*[\!\@\#\$\%\^\&\*\(\)\_\+\~\`\'\;\.\,\<\>\[\]\\\|\=\-])/)

        if (validPasswordOneSpecialCharacter == null) {
            passwordError.push("Hasło musi zawierać co najmniej jeden znak specjalny. \n");
        }

        if (validPasswordLength == null || validPasswordOneUpperCharacter == null || validPasswordOneDigit == null || validPasswordOneSpecialCharacter == null) {
            this.setState({
                passwordIsValid: false,
                passwordValidationError: passwordError
            });
        }
        else {
            this.setState({
                passwordIsValid: true,
                passwordValidationError: []
            });
        }
    }

    validateDate(e) {
        let validDate = this.state.date.match(/^Od\s([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1])(?:(?:\s([0-1]\d|[2][0-3])\:([0-5]\d)(?::([0-5]\d))?)?)\sdo\s([1-2]\d{3})\-([0]\d|[1][0-2])\-([[0-2]\d|[3][0-1])(?:(?:\s([0-1]\d|[2][0-3])\:([0-5]\d)(?::([0-5]\d))?)?)$/)
        if (validDate == null) {
            this.setState({
                dateIsValid: false,
                dateValidationError: "Wprowadzono zakres dat w niepoprawnym formacie."
            });
        }
        else {
            this.setState({
                dateIsValid: true,
                dateValidationError: []
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
                        <Input name="email" id="emailId" invalid={(!this.state.emailIsValid && this.state.emailValidationError.length != 0) ? true : false} valid={this.state.emailIsValid} onChange={e => this.changeValue(e)} value={this.state.email} onBlur={e => this.validateEmail(e)} />
                        <FormFeedback>{this.state.emailValidationError}</FormFeedback>
                    </FormGroup>

                    <Row form>
                        <Col md={6}>
                            <FormGroup className="formGroup">
                                <Label for="firstName">Imię</Label>
                                <Input type="string" name="firstName" id="firstNameId" invalid={(!this.state.firstNameIsValid && this.state.firstNameValidationError.length != 0) ? true : false} valid={this.state.firstNameIsValid} onChange={e => this.changeValue(e)} value={this.state.firstName} onBlur={e => this.validateFirstName(e)} />
                                <FormFeedback>{this.state.firstNameValidationError}</FormFeedback>
                            </FormGroup>
                        </Col>

                        <Col md={6}>
                            <FormGroup className="formGroup">
                                <Label for="lastName">Nazwisko</Label>
                                <Input type="string" name="lastName" id="lastNameId" invalid={(!this.state.lastNameIsValid && this.state.lastNameValidationError.length != 0) ? true : false} valid={this.state.lastNameIsValid} onChange={e => this.changeValue(e)} value={this.state.lastName} onBlur={e => this.validateLastName(e)} />
                                <FormFeedback>{this.state.lastNameValidationError}</FormFeedback>
                            </FormGroup>
                        </Col>
                    </Row>

                    <FormGroup className="formGroup">
                        <Label for="date">Data rezerwacji</Label>
                        <Alert color="info" className="alertFormat">
                            Od YYYY-MM-DD [HH:MM:SS] do YYYY-MM-DD [HH:MM:SS]
                         </Alert>
                        <Input type="string" name="date" id="dateId" invalid={(!this.state.dateIsValid && this.state.dateValidationError.length != 0) ? true : false} valid={this.state.dateIsValid} onChange={e => this.changeValue(e)} onChange={e => this.changeValue(e)} value={this.state.date} onBlur={e => this.validateDate(e)} />
                        <FormFeedback>{this.state.dateValidationError}</FormFeedback>
                    </FormGroup>

                    <Row form>
                        <Col sm="12" md={{ size: 6, offset: 3 }}>
                            <FormGroup className="formGroup">
                                <Label for="password">Hasło</Label>
                                <Input type="password" name="password" id="passwordId" invalid={(!this.state.passwordIsValid && this.state.passwordValidationError.length != 0) ? true : false} valid={this.state.passwordIsValid} onChange={e => this.changeValue(e)} value={this.state.password} onBlur={e => this.validatePassword(e)} />
                                <FormFeedback>
                                    <ul>
                                        {this.state.passwordValidationError.map(error => <li>{error}</li>)}
                                    </ul>
                                </FormFeedback>
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


