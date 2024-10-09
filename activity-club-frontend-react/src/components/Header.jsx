import Button from "./Button";
export default function Header({
  selectedPage,
  id,
  handlePageChange,
  handleSignOut,
}) {
  return (
    <header id={id}>
      <h1>Adventure Time</h1>
      <nav>
        <ul>
          <Button
            isActive={selectedPage === "Home"}
            handlePageChange={() => handlePageChange("Home")}
          >
            Home
          </Button>
          <Button
            isActive={selectedPage === "Events"}
            handlePageChange={() => handlePageChange("Events")}
          >
            Events
          </Button>
          <Button
            isActive={selectedPage === "Guides"}
            handlePageChange={() => handlePageChange("Guides")}
          >
            Guides
          </Button>

          <Button isActive={false} handleSignOut={handleSignOut}>
            Sign Out
          </Button>
        </ul>
      </nav>
    </header>
  );
}
