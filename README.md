# Azure Functions by Real Use Cases

Azure Functions are at the heart of what Serverless computing is, enabling you to care only for your owncode, paying per execution and scaling at massive scale out of the box.

In this soluton we will go through some common use cases and how we can leverage Azure Functions to implement those solutions easily and cost effective.

We will cover:

* **Email Sender:** Integrating with SendGrid, we will create an email sender ready to use in 10 minutes. We will cover an synchonous version using a HTTP Api and an asynchonous version listening messages from an Azure Storage Queue.

* **Image Resizer:** We will create a solution that will react to new images uploaded to an Azure Storage Blob and will resize and store them to be consumed.

* **Scheduled Tasks:** We will design a clean up task to run daily so that we can keep our storage costs at minumun

* **File Importer:** With no FTP nor API client calls involved, we will use an event-oriented approach in order to listen to files uploaded to an Azure Blob Storage and will import them into Azure Table Storage.

Additionally, we will also set up a local environment Azure Functions SDK, Azure Storage Emulator and Azure Storage Explorer.