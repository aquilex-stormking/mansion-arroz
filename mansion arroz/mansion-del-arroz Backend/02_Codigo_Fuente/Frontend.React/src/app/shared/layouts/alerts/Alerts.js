import React from "react";
import "./Alerts.css";
import { Alert } from "react-bootstrap";
import AlertsService from "../../../services/Alerts.Service";

export default class Alerts extends React.Component {
  subscription;

  constructor(props) {
    super(props);
    this.state = {
      alerts: [],
    };
  }

  componentDidMount() {
    this.subscription = AlertsService.onAlert(this.props.id).subscribe((alert) => {
      // add alert to array
      this.setState({ alerts: [...this.state.alerts, alert] });

      //close alert automatically
      if (alert.autoClose) {
        setTimeout(() => this.removeAlert(alert), 10000);
      }
    });
  }

  componentWillUnmount() {
    // unsubscribe & unlisten to avoid memory leaks
    this.subscription.unsubscribe();
  }

  removeAlert(alert) {
    this.setState({ alerts: this.state.alerts.filter((x) => x !== alert) });
  }

  render() {
    const { alerts } = this.state;
    if (!alerts.length) return null;
    return (
      <div className="alerts position-fixed mt-3 right w-alert">
        {alerts.map((alert, index) => {
          return (
            <Alert key={index} variant={alert.type} onClose={() => this.removeAlert(alert)} dismissible>
              <strong>{alert.message}</strong>
            </Alert>
          );
        })}
      </div>
    );
  }
}
