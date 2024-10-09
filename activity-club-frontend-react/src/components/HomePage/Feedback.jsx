import { useEffect, useState } from "react";
import axios from "axios";

export default function Feedback({ memberEmail }) {
  const [fdPosts, setFdPosts] = useState([]);
  const [feedback, setFeedback] = useState("");
  const [postAdded, setPostAdded] = useState(false);
  const [isValidFeedback, setIsValidFeedback] = useState(true);
  const [feedbackNonEmpty, setFeedbackNonEmpty] = useState(true);

  useEffect(() => {
    fetchPosts();
  }, []);
  useEffect(() => {
    if (postAdded) {
      fetchPosts();
      setPostAdded((pa) => !pa);
    }
  }, [postAdded]);
  const fetchPosts = async () => {
    const response = await axios.get(
      "http://localhost:5004/Member/GetFeedbacks"
    );

    setFdPosts((fp) => [...response.data]);
  };

  const handleFeedbackChange = (event) => {
    setFeedback((f) => (f = event.target.value));
    if (feedback.length > 60) {
      setIsValidFeedback(false);
    } else {
      setIsValidFeedback(true);
    }
  };

  const handleFeedbackSubmission = async (event) => {
    event.preventDefault();
    const feedbackPost = {
      Email: memberEmail,
      MemberPost: feedback,
    };

    if (isValidFeedback && feedback !== "") {
      const response = await axios.post(
        "http://localhost:5004/Member/Submit Feedback",
        feedbackPost
      );
      setFeedback((f) => (f = ""));
      setPostAdded((pa) => !pa);
      setFeedbackNonEmpty(true);
    } else if (feedback === "") {
      setFeedbackNonEmpty(false);
    } else {
      setFeedbackNonEmpty(true);
    }
  };

  return (
    <>
      <div id="feedback-form-box">
        <h2>Make a feedback!</h2>
        {isValidFeedback ? null : (
          <p className="text-danger" id="feedback-validation">
            Feedbacks must not exceed 60 characters in length!
          </p>
        )}
        {feedbackNonEmpty ? null : (
          <p className="text-danger">Please submit a non-empty feedback</p>
        )}

        <div id="feedback-form">
          <form>
            <textarea
              name="feedback-input"
              required
              placeholder="Give us a feedback!"
              onChange={handleFeedbackChange}
              value={feedback}
              style={{ backgroundColor: isValidFeedback ? "" : "red" }}
            ></textarea>
            <button onClick={handleFeedbackSubmission}>submit</button>
          </form>
        </div>
      </div>

      <div id="feedbacks">
        <h2>Feedbacks</h2>
        <div id="feedback-posts">
          <ul>
            {fdPosts.map((feedbackPost, index) => {
              return (
                <li key={index}>
                  <div className="post-info">
                    <img
                      className="member-photo"
                      src={`data:image/jpeg;base64,${feedbackPost.photo}`}
                      alt="Member photo"
                    />
                    <div className="post-top">
                      <h3>
                        <strong>{feedbackPost.memberName}</strong>
                        <div className="post-description">
                          <p>{feedbackPost.description}</p>
                        </div>
                      </h3>

                      <span> {feedbackPost.createdDate}</span>
                    </div>
                  </div>
                </li>
              );
            })}
          </ul>
        </div>
      </div>
    </>
  );
}
