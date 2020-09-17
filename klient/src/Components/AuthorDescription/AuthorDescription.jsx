import React from "react"
import { Col,Row } from "reactstrap"
import styles from './AuthorDescription.module.css'
import footer from '../Footer/Footerstyle.module.css'
export default function AuthorDesc(props)
{ 
    const authors =[
    {id: 1, name: "Adam Jochemczyk", role: "frontend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t1.0-9/67827335_1114327365428495_5081469124652040192_o.jpg?_nc_cat=104&_nc_sid=09cbfe&_nc_ohc=ej58UdRJi44AX9behFw&_nc_ht=scontent.fwaw7-1.fna&oh=71587f2327ab5fc4138eaca00e7e471c&oe=5F30B3B5"},
    {id: 2, name: "Bogdan Kucher", role: "backend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t31.0-8/27355853_1860336884276612_8053680163315302839_o.jpg?_nc_cat=110&_nc_sid=09cbfe&_nc_ohc=KxYhnQXwEAoAX_F6r8s&_nc_ht=scontent.fwaw7-1.fna&oh=29b0b853e43d2bbaa30318fec93a1d15&oe=5F2FC141"},
    {id: 3, name: "Żaneta Borowska", role: "backend", src: "https://scontent.fwaw7-1.fna.fbcdn.net/v/t1.0-9/16298446_1290156867698087_3066057403762340950_n.jpg?_nc_cat=101&_nc_sid=09cbfe&_nc_ohc=EqX7hMQAnrYAX9LSpzm&_nc_ht=scontent.fwaw7-1.fna&oh=d33c5d6b5ebfeb8a18c235173393dbe3&oe=5F2F29C4"},
];

function CreateAuthor(authors){
    return (
        <Col sm="4" key={authors.id}>
        <img className={styles.avatar} src={authors.src} alt="author profile"/>
            <br/>
            {authors.name} - {authors.role}
            </Col>
            )
}

    return (
        <div className={footer.global}>
        <h5 className="pt-2">Authors</h5>
        <hr />
        <Row className="pb-2">
        {authors.map(CreateAuthor)}
        </Row>
        <hr/>
        </div>
    )
}