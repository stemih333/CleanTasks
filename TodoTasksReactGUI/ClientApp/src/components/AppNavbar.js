import React from 'react';
import {
    Collapse,
    Navbar,
    NavbarToggler,
    NavbarBrand,
    Nav,
    NavItem,
    NavLink
} from 'reactstrap';

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
                <NavbarBrand href="/home">Todo Tasks</NavbarBrand>
                <NavbarToggler onClick={this.toggle.bind(this)} />
                <Collapse isOpen={this.state.isOpen} navbar>
                    <Nav navbar>
                        <NavItem >
                            <NavLink href="/tasks">Tasks</NavLink>
                        </NavItem>
                        <NavItem >
                            <NavLink href="/permissions">Permissions</NavLink>
                        </NavItem>
                    </Nav>
                    
                </Collapse>
            </Navbar>
        );
    }
}