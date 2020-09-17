import React from "react"
import {Row, Col, Container, Button} from "reactstrap"
import styles from './Homestyle.module.css'
import calendar from '../img/calendar.png'
import target from '../img/target.png'
import green from '../img/green.png'
import Feature from '../Feature/Feature'


const features =[
    {id: 1, icon: calendar, text: "Simple check calendar and book available car"},
    {id: 2, icon: target, text: "Book car fits to your needs"},
    {id: 3, icon: green, text: "By sharing car you safe our planet"}
];

function CreateFeatures(features){
    return (
        <Feature 
        key={features.id}
        icon={features.icon}
        text={features.text}
        />
    )
}

export default function Home(){

    return (
        <Container>
        <Row>
            <Col sm={6}>
            <div className={styles.top}>
                <h1>Book a company car</h1>
                <p>Find the right car fits to your needs</p>
                <Button color="primary"><a href='/reserve-car' className={styles.link} style={{textDecoration: 'none', color: 'white'}}> Book a car</a></Button>
            </div>
            </Col>
            <Col sm={6} className="justify-content-center">
                <img src="https://pngimg.com/uploads/mazda/mazda_PNG133.png" alt="car" className={styles.image}/>
            </Col>
        </Row>
        <Row className={styles.center}>
        <Col>
        <h1>Features</h1>
        </Col>
        </Row>
        <Row className={styles.center}>
        {features.map(CreateFeatures)}
        </Row>
        </Container>
    )
}