import React from "react"
import Feature from "./Feature"
import { shallow } from "enzyme"

test("Feature renders correctly", ()=>{
    const wrapper=shallow(
        <Feature
            icon="https://image.flaticon.com/icons/svg/3260/3260811.svg"
            text="example text"
        />
    )
    expect(wrapper).toMatchSnapshot()
})