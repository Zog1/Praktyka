import React from 'react';
import './App.css';
import { BrowserRouter as Router, Switch, Route, Redirect} from 'react-router-dom'; 
import 'bootstrap/dist/css/bootstrap.css';
import Login from './Components/Login/Login'
import GenericNotFound from './Components/GenericNotFound/GenericNotFound'
import SetPassword from './Components/SetPassword/SetPassword';
import Home from "./Components/Home/Home";
import ReserveCar from "./Components/ReserveCar/ReserveCar";
import Booking from './Components/Booking/Booking'
import UserManager from './Components/UserManager/UserManager'
import EditUser from './Components/EditUser/EditUser'
import FaultManager from './Components/FaultManager/FaultManager'
import CarManager from './Components/CarManager/CarManager'
import EditCar from './Components/EditCar/EditCar'
import AdminHistory from './Components/AdminHistory/AdminHistory'
import Map from './Components/Map/Map'
import UserHistory from './Components/UserHistory/UserHistory'
import UserReportFault from './Components/UserReportFault/UserReportFault'
import UserHeader from './Components/UserHeader/UserHeader'
import AdminHeader from './Components/AdminHeader/AdminHeader';
import Footer from './Components/Footer/Footer'

function App() {
  let userRole=sessionStorage.getItem('userRole');
  let isLoggedIn=sessionStorage.getItem("isLoggedIn");

  return (
    <Router>
        {isLoggedIn ? 
        (userRole ? 
        (
          userRole==="user" && <div>
          <UserHeader />
          <Switch>
          <Route exact path='/' component={Login}/>
          <Route exact path="/home" component={Home} />
          <Route exact path="/reserve-car" component={ReserveCar} />
          <Route path="/reserve-car/booking" component={Booking} />
          <Route exact path='/history' component={UserHistory} />
          <Route path="/history/map" component={Map} />
          <Route path="/history/report" component={UserReportFault} />
          <Route component={GenericNotFound} />
          </Switch>
          <Footer/>
          </div>
        ) ||
         (
           userRole==="admin" && <div>
           <AdminHeader />
           <Switch>
           <Route exact path='/' component={Login}/>
           <Route exact path='/admin' component={Home} />
           <Route exact path='/home' component={Home} />
           <Route exact path='/history' component={UserHistory} />
           <Route path="/history/map" component={Map} />
          <Route path="/history/report" component={UserReportFault} />
          <Route path="/admin/reserve-car/booking" component={Booking} />
          <Route exact path="/admin/user-manager" component={UserManager} />
          <Route path="/admin/user-manager/edit" component={EditUser} />
          <Route path="/admin/add-user" component={EditUser} />
          <Route exact path="/admin/defects-manager" component={FaultManager} />
          <Route exact path="/admin/car-manager" component={CarManager} />
          <Route path="/admin/car-manager/edit" component={EditCar} />
          <Route path="/admin/add-car" component={EditCar} />
          <Route path="/admin/admin-history" component={AdminHistory} />
          <Route path='/admin/user-history' component={UserHistory} />
          <Route path='/admin/user-history/map' component={Map} />
          <Route exact path="/reserve-car" component={ReserveCar} />
          <Route path="/reserve-car/booking" component={Booking} />
          <Route exact path='/history' component={UserHistory} />
          <Route path="/history/map" component={Map} />
          <Route path="/history/report" component={UserReportFault} />
          <Route component={GenericNotFound} />
          </Switch>
          <Footer/>
           </div>
           ): <Redirect to='/' />)
         : <div>
         <Switch>
         <Route exact path='/' component={Login}/>
         <Route path='/set-password/:code' component={SetPassword} />
         <Route component={GenericNotFound} />
         </Switch>
         </div>}
    </Router>
  );
}

export default App;
