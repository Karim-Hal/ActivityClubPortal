export default function EventFilters({ handleFilterChange }) {
  return (
    <li>
      <select
        name="category-filter"
        id="category-filter"
        className="event-filter"
        onChange={(e) => handleFilterChange(0, e.target.value)}
      >
        <option value="0">Select Category</option>
        <option value="1">Monster Hunting</option>
        <option value="2">Magic Training</option>
        <option value="3">Treasure Hunts</option>
        <option value="4">Hero Training</option>
        <option value="5">Royal Gatherings</option>
        <option value="6">Mushroom Forest Expeditions</option>
        <option value="7">Potion Brewing</option>
        <option value="8">Epic Journeys</option>
        <option value="9">Dungeon Crawling</option>
      </select>

      <select
        name="status-filter"
        id="status-filter"
        className="event-filter"
        onChange={(e) => handleFilterChange(1, e.target.value)}
      >
        <option value="Select Event Status">Select Event Status</option>
        <option value="Open">Open</option>
        <option value="Closed">Closed</option>
      </select>

      <select
        name="date-filter"
        id="date-filter"
        className="event-filter"
        onChange={(e) => handleFilterChange(2, e.target.value)}
      >
        <option value="Sort by date">Sort by date</option>
        <option value="asc">Ascending</option>
        <option value="desc">Descending</option>
      </select>
      <div className="form-check form-switch">
        <input
          className="form-check-input"
          type="checkbox"
          role="switch"
          id="flexSwitchCheckDefault"
          onChange={(e) => handleFilterChange(3, e.target.checked)}
        />
        <label className="form-check-label" htmlFor="flexSwitchCheckDefault">
          Show Registered Events
        </label>
      </div>
    </li>
  );
}
