import { Subject } from "rxjs";
import { filter } from "rxjs/operators";

const alertSubject = new Subject();
const defaultId = "default-alert";

export default class AlertsService {
  static onAlert(id = defaultId) {
    return alertSubject.asObservable().pipe(filter((x) => x && x.id === id));
  }

  static success(message, options = { autoClose: true, keepAfterRouteChange: true }) {
    this.alert({ ...options, type: "success", message });
  }

  static info(message, options = { autoClose: true, keepAfterRouteChange: true }) {
    this.alert({ ...options, type: "info", message });
  }

  static warning(message, options = { autoClose: true, keepAfterRouteChange: true }) {
    this.alert({ ...options, type: "warning", message });
  }

  static error(message, options = { autoClose: true, keepAfterRouteChange: true }) {
    this.alert({ ...options, type: "danger", message });
  }

  static publicAlert(type, message, options = { autoClose: true, keepAfterRouteChange: true }) {
    this.alert({ ...options, type, message });
  }

  static alert(alert) {
    alert.id = alert.id || defaultId;
    alertSubject.next(alert);
  }

  static clear(id = defaultId) {
    alertSubject.next({ id });
  }
}
