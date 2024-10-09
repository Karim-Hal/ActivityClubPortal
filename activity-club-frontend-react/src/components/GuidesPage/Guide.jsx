export default function Guide(props) {
  return (
    <li className="guide-card" onClick={props.handleEditGuide}>
      <img src={`data:image/jpeg;base64,${props.photo}`} alt="guide image" />
      <ul>
        <li> Email: {props.email}</li>
        <li>Name: {props.fullName}</li>
        <li>Date of Birth: {props.dateOfBirth}</li>
        <li>Joining Date: {props.joiningDate}</li>
        <li>Profession: {props.profession}</li>
        {props.canDelete ? (
          <li className="trashcan">
            <i
              className="fa-solid fa-trash-can fa-2x"
              onClick={(e) => {
                e.stopPropagation(); // Prevent the edit modal from opening
                props.handleDeleteGuide();
              }}
            ></i>
          </li>
        ) : null}
      </ul>
    </li>
  );
}
