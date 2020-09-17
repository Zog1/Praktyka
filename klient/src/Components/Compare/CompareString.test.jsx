import compareString from "../Compare/CompareString"

it('String compare should return true', () => {
    let res=compareString("ABC","abc")
    expect(res).toBe(true);
  });