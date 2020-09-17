import CarImageParams from './CarImageParams'
import {shallow} from "enzyme"
import React from "react"

it("CarImageParams render correctly", ()=>{
    
    const wrapper = shallow(
        <CarImageParams
            doorsnumber={3}
            sitsnumber={3}
            yearOfProduction={2019}
        />
    )
    expect(wrapper).toMatchSnapshot()
})