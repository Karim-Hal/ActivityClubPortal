export default function Button({
  children,
  isActive,
  handlePageChange,
  handleSignOut,
}) {
  return (
    <li>
      <button
        className={isActive ? "active-header-btn" : null}
        onClick={
          children !== "Sign Out"
            ? () => handlePageChange(children)
            : handleSignOut
        }
      >
        {children}
      </button>
    </li>
  );
}
