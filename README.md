# StoEtDash
Stock and ETF portfolio tracker, that was used as university project for PV178 Introduction to C#/.NET development.



## Video showcase of the application


### Registration
- Registration consists of username, password and repeated password as there was no need to add another fields such as e-mail, phone number etc.
- Notification is shown in case username is already in use.

- https://user-images.githubusercontent.com/46026094/234720269-7c33a789-b9a3-4981-85bd-fe80e5344850.mp4


### Login
- Notifications are shown in case the user does not exist, or the password does not match with the saved one.

- https://user-images.githubusercontent.com/46026094/234720327-2a9f079b-5839-4717-87e2-a0f345e00573.mp4


### Dashboard - buy asset
- The form to add buy transaction is handled with error messages, in case something is missing or in wrong format.
- To pick stock or ETF the user must use search which is provided via **ticker search** endpoint from [AlphaVentage](https://www.alphavantage.co/documentation/).

- https://user-images.githubusercontent.com/46026094/234720572-20f37c4d-e410-4fcb-a032-8c8135ddb8fe.mp4


### Dashboard - sell asset
- Once there are some assets, there is possibility to add sell transactions.
- Similarly, like buy form also sell form is handled with error messages, in case something is missing or in wrong format.
- One special case, is that there is check whether there are enough shares owned to sell.

- https://user-images.githubusercontent.com/46026094/234720583-f596a836-2d5d-4210-9507-7eeb054d54f6.mp4


### Dashboard - edit and delete transaction
- Each asset displayed in the table is clickable and the table of transactions is shown after click on the asset.
- In the list of transactions, there are icons for edit and delete of transaction.
- Edit of the transaction is handled with error messages, when something is missing or in wrong format.
- During edit of the sell transaction and delete of buy transaction, there is check whether there are enough shares owned.

- https://user-images.githubusercontent.com/46026094/234720595-e7e93494-34ce-4c8b-ae72-041ca6e4063a.mp4


### Dashboard - asset table filters
- There is possibility to filter table of assets.
- **Show only underpriced assets** will hide assets, which average price is lower than current share price.
- Select **Asset types** will show only assets that has or hasn't some dividends or all of the assets.

- https://user-images.githubusercontent.com/46026094/234720621-e859339c-d40f-4262-abd7-d241b244d371.mp4


### Dashboard - charts
- There are 3 charts, while 2 of them consists of more charts controlled by select.
- The first chart shows assets divided by their current value or by number of shares.
- The second chart shows daily/weekly/monthly price development.
- The third chart shows the ratio of assets with dividends vs assets without them.
- All charts are interactive, so you can hide or show a specific dataset or item.

- https://user-images.githubusercontent.com/46026094/234720636-03f84929-d9ea-46d9-96c9-7d5b899dfab8.mp4

