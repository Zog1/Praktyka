import React from "react"
import GenericNotFound from './GenericNotFound'
import { shallow } from 'enzyme'

test('GenericNotFound renders correctly', ()=>{
    const wrapper=shallow(<GenericNotFound />)
    expect(wrapper).toMatchSnapshot()
})