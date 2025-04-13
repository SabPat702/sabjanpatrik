import React, { useState, useEffect } from 'react';
 
const ChatPage = () => {
    const [posts, setPosts] = useState([]);
    const [newPost, setNewPost] = useState("");
    const username = localStorage.getItem('username') || 'Vendég';
 
    const fetchPosts = async () => {
        const res = await fetch('http://localhost:3001/chat');
        const data = await res.json();
        setPosts(data);
    };
 
    useEffect(() => {
        fetchPosts();
    }, []);
 
    const handlePost = async () => {
        const res = await fetch('http://localhost:3001/chat', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ author: username, text: newPost })
        });
 
        if (res.ok) {
            setNewPost('');
            fetchPosts(); // újratöltjük a bejegyzéseket
        }
    };
 
    const handleReport = async (postId) => {
        await fetch(`http://localhost:3001/chat/${postId}/report`, {
            method: 'POST'
        });
        alert("Bejegyzés jelentve!");
    };
 
    return (
        <div className="chat-page">
            <h1>Chat Room</h1>
            <textarea
                value={newPost}
                onChange={(e) => setNewPost(e.target.value)}
                placeholder="Write your post..."
            />
            <button onClick={handlePost}>Post</button>
            <div className="posts">
                {posts.map(post => (
                    <div key={post.id} className="post">
                        <p><strong>{post.author}</strong>: {post.text}</p>
                        <button onClick={() => handleReport(post.id)}>Report</button>
                        {/* Itt lehet hozzászólásokat is hozzáadni majd */}
                    </div>
                ))}
            </div>
        </div>
    );
};
 
export default ChatPage;