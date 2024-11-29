from flask import Flask, request, jsonify
from flask_sqlalchemy import SQLAlchemy
import os
import uuid
from flask_cors import CORS

app = Flask(__name__)

# Use a direct database URL for testing
app.config['SQLALCHEMY_DATABASE_URI'] = 'postgresql://your-username:your-password@your-neon-host/your-database'
db = SQLAlchemy(app)
CORS(app)

class SaveFile(db.Model):
    __tablename__ = 'save_file'
    id = db.Column(db.String(8), primary_key=True)
    save_data = db.Column(db.JSON, nullable=False)
    created_at = db.Column(db.DateTime, server_default=db.func.now())

def init_db():
    with app.app_context():
        # Drop existing tables to ensure clean state
        db.drop_all()
        # Create tables
        db.create_all()

@app.route('/api/saves', methods=['POST'])
def create_save():
    save_data = request.json
    share_code = str(uuid.uuid4())[:8]
    
    new_save = SaveFile(id=share_code, save_data=save_data)
    db.session.add(new_save)
    db.session.commit()
    
    return jsonify({
        "data": {
            "share_code": share_code
        }
    })

@app.route('/api/saves/<share_code>', methods=['GET']) 
def get_save(share_code):
    save = SaveFile.query.get_or_404(share_code)
    return jsonify({
        "data": save.save_data
    })

# Initialize database before running app
init_db()

if __name__ == '__main__':
    app.run(debug=True) 