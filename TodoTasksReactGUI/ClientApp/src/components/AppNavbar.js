import React from 'react';
import {
    Collapse,
    Navbar,
    NavbarToggler,
    Nav,
    NavItem
} from 'reactstrap';
import { Link, NavLink as LinkNav } from 'react-router-dom';

export default class AppNavbar extends React.Component {
    constructor(props) {
        super(props);

        this.state = {
            isOpen: false
        };
    }

    toggle() {
        this.setState({
            isOpen: !this.state.isOpen
        });
    }

    render() {
        return (
            <Navbar color="light" light expand="md">
                <Link to="/" className="navbar-brand">Todo Tasks</Link>
                <NavbarToggler onClick={this.toggle.bind(this)} />
                <Collapse isOpen={this.state.isOpen} navbar>
                    <Nav navbar>
                        <NavItem >
                            <LinkNav to="/tasks" className="nav-link">Tasks</LinkNav>
                        </NavItem>
                        <NavItem >
                            <LinkNav to="/permissions" className="nav-link">Permissions</LinkNav>
                        </NavItem>
                    </Nav>
                    
                </Collapse>
            </Navbar>
        );
    }
}