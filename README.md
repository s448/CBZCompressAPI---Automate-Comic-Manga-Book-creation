# macOS CBZ Compressor App - API Documentation

## Overview

This documentation covers the API for the macOS CBZ Compressor App. The application is designed to automate the compression of image subfolders into `.cbz` archives and synchronize them with cloud storage. This document will outline the available API endpoints, their usage, and the steps to run the application.

---

## Endpoints

### **Scan Folders**

* **Endpoint:** `/api/scan`
* **Method:** `POST`
* **Description:** Scans the root directory for subfolders containing images.
* **Request Body:**

  ```json
  {
    "path": "<root_folder_path>",
    "batchSize": 10,
    "stopAfter": 50
  }
  ```
* **Response:**

  ```json
  {
    "scannedFolders": ["folder1", "folder2"],
    "total": 2
  }
  ```

---

### **Compress Folder**

* **Endpoint:** `/api/compress`
* **Method:** `POST`
* **Description:** Compresses the specified folder into `.cbz` format.
* **Request Body:**

  ```json
  {
    "folderPath": "<folder_path>"
  }
  ```
* **Response:**

  ```json
  {
    "status": "success",
    "outputPath": "<output_folder>/folder_name.cbz"
  }
  ```

---

### 3️⃣ **Sync to Cloud**

* **Endpoint:** `/api/sync`
* **Method:** `POST`
* **Description:** Syncs the compressed CBZ file to the specified cloud directory.
* **Request Body:**

  ```json
  {
    "filePath": "<cbz_file_path>",
    "cloudProvider": "iCloud" | "Dropbox" | "GoogleDrive"
  }
  ```
* **Response:**

  ```json
  {
    "status": "synced",
    "cloudPath": "<cloud_folder_path>"
   } 
