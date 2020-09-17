import React from "react"
import Footer from "./Footer"
import { shallow } from "enzyme"
import useFooter from "./useFooter.utils"

test("Footer renders correctly", ()=>{
    const wrapper=shallow(
        <Footer/>
    )
    expect(wrapper).toMatchSnapshot()
})
