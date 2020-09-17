import React from "react"
import AdminHeader from "./AdminHeader"
import { shallow } from "enzyme"

it("AdminHeader render correctly", ()=>{
    const wrapper = shallow(
        <AdminHeader/>
    )
    expect(wrapper).toMatchSnapshot()
})