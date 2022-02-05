import { initializeApp } from "firebase/app";
import { getFirestore } from 'firebase/firestore';
import { getAuth, signInWithPopup, GoogleAuthProvider } from 'firebase/auth';

const firebaseConfig = {
    apiKey: "77dcdf90ba570b29e9928e4b0ce0f3af7870019e",
    authDomain: "delete-project-c7021.firebaseapp.com",
    projectId: "gisa-c54d2",
    storageBucket: "delete-project-c7021.appspot.com",
    messagingSenderId: "688102987186",
    appId: "1:688102987186:web:e27537ggg7abc2b86b982b"
};

const app = initializeApp(firebaseConfig);
export const db = getFirestore(app);
export const auth = getAuth();
export const provider = new GoogleAuthProvider();