export default function FirstView({ handleViewClick }) {
  return (
    <div id="first-view">
      <h1>You are...</h1>
      <div id="role-container">
        <div className="view-button" onClick={() => handleViewClick("Admin")}>
          <h2>An Admin</h2>
        </div>
        <div className="view-button" onClick={() => handleViewClick("User")}>
          <h2>A User</h2>
        </div>
      </div>
    </div>
  );
}
