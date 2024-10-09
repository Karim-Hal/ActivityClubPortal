export default function Event(props) {
  return (
    <>
      {props.canDelete ? (
        <li
          className="guide-card"
          onClick={props.handleEditEvent}
          key={props.keyIdx}
        >
          <img src={props.imgSrc} alt={props.name} />
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
      ) : (
        <li onClick={() => props.handleEventClick(props.keyIdx)}>
          <div>
            <img src={props.imgSrc} alt={props.name} className="event-img" />
          </div>
          <div className="event-card-info">
            <div className="event-card-top-info">
              <h2>{props.name}</h2>
              <p>{props.status}</p>
            </div>
            <div className="event-description">{props.description}</div>
            <div>Starting Date: {props.dateFrom}</div>
          </div>
        </li>
      )}
    </>
  );
}
