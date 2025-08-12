import { DataTypes } from "sequelize";
import  _User  from "./User.js";

export default function initModels(sequelize) {
  var User = _User(sequelize, DataTypes);

  return {
    User
  };
}
