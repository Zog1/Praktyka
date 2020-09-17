import Home from './Home'
import React from "react"
import {shallow} from "enzyme"

test("Home component renders correctly",()=>{
    const wrapper=shallow(<Home/>)
    expect(wrapper).toMatchSnapshot()
})