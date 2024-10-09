export default function Admin() {
  return (
    <li
      className="guide-card"
      onClick={props.handleEditEvent}
      key={props.keyIdx}
    >
      <ul>
        <li key={1}>Name: {props.name}</li>
        <li key={2}>Description: {props.description}</li>
        <li key={3}>Destination: {props.destination}</li>
        <li key={4}>Date From: {props.dateFrom}</li>
        <li key={5}>Date To: {props.dateTo}</li>
        <li key={6}>Cost: {props.cost}</li>
        <li key={7}>Status: {props.status}</li>
        <li key={8}>Category Id: {props.categoryId}</li>

        <li className="trashcan" key={9}>
          <i
            className="fa-solid fa-trash-can fa-2x"
            onClick={(e) => {
              e.stopPropagation(); // Prevent the edit modal from opening
              props.handleDeleteEvent();
            }}
          ></i>
        </li>
      </ul>
    </li>
  );
}
