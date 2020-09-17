import React from "react"
import CardCar from "./CardCar"
import { shallow } from "enzyme"

test("CardCar renders correctly", ()=>{
    const wrapper=shallow(
        <CardCar 
            key={1}
            src="a"
            brand="brand"
            model="model"
            registrationNumber="STA12345"
            doors={3}
            sits={2}
            yearOfProduction="2012"
        />
    )
    expect(wrapper).toMatchSnapshot()
})