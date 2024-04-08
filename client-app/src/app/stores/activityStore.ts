import { makeAutoObservable } from "mobx"

export default class ActivityStore {
    title = "Hello from MobX!"

    constructor() {
        makeAutoObservable(this)
    }

    setTitle = () => {
        this.title = this.title + '!'
    }
}


//MobX core functions:
//====================
//intialization = makeAutoObservable(this) in the class constructor

//observable = states/properties
//action = setter/normal functions
//computed = getter functions
//reaction = "reacts" to observable state changes. takes 2 parameters; function/expression to identify what state we want to observer, and the function that does something with that state that's changing