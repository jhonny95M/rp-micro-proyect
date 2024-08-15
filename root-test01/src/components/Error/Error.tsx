import React from "react";
import "./Error.css";

const Error: React.FC = (props: any) => {
  return (
    <div className="error-page">
      <img
        className="error-page-img"
        src="https://cdn.canopytax.com/images/ErrorState.svg"
        style={{ width: "15%", padding: "16px 0" }}
      />
      <p className="error-page-message error-page-p">
        Ocurrió un error. Porfavor, intente hacer click en el botón para recargar la aplicación.{" "}
      </p>
      {/* <p className="error-page-message error-page-p">Detalle: {props?.error.message}</p> */}
      <br></br>
      <button className="error-page-button" onClick={props?.resetErrorBoundary}>
        Recargar Página
      </button>
    </div>
  );
};

export default Error;