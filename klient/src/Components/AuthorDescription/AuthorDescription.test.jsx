import React from "react"
import AuthorDescription from "./AuthorDescription"
import { shallow } from "enzyme"

test("AuthorDescription renders correctly", ()=>{
    const wrapper=shallow(<AuthorDescription/>)

    expect(wrapper).toMatchSnapshot()
})