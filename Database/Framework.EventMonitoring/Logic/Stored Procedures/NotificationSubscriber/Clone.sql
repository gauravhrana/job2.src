			IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationSubscriberClone')
			BEGIN
				PRINT 'Dropping Procedure NotificationSubscriberClone'
				DROP  Procedure NotificationSubscriberClone
			END 
			GO

			PRINT 'Creating Procedure NotificationSubscriberClone'
			GO

			/*********************************************************************************************
			**		File: 
			**		Name: NotificationSubscriberClone
			**		Desc: 
			**
			**		This template can be customized:
			**              
			**		Return values:
			** 
			**		Called by:   
			**              
			**		Parameters:
			**		Input							Output
			**     ----------							-----------
			**
			**		Auth: 
			**		Date: 
			*********************************************************************************************
			**		Change History
			*********************************************************************************************
			**		Date:		Author:				Description:
			**		--------	--------			------------------------------------------------------
			**		
			**********************************************************************************************/

			CREATE Procedure dbo.NotificationSubscriberClone
			(
					@NotificationSubscriberId			INT			= NULL 	OUTPUT	
				,	@ApplicationId			INT			= NULL
				,	@Name					VARCHAR(50)						
				,	@Description			VARCHAR(100)						
				,	@SortOrder				INT								
				,	@AuditId				INT									
				,	@AuditDate				DATETIME	= NULL				
				,	@SystemEntityType		VARCHAR(50) = 'NotificationSubscriber'			
			)
			AS
			BEGIN		

				SELECT	@ApplicationId		= ISNULL(@ApplicationId, ApplicationId)
					,	@Description		= ISNULL(@Description, Description)
					,	@SortOrder			= ISNULL(@SortOrder, SortOrder)				
				FROM	dbo.NotificationSubscriber
				WHERE	NotificationSubscriberId		= @NotificationSubscriberId

				EXEC dbo.NotificationSubscriberInsert 
						@NotificationSubscriberId		=	NULL
					,	@ApplicationId		=	@ApplicationId
					,	@Name				=	@Name
					,	@Description		=	@Description
					,	@SortOrder			=	@SortOrder
					,	@AuditId			=	@AuditId

				-- Create Audit Record
				EXEC dbo.AuditHistoryInsert
						@SystemEntityType		= @SystemEntityType
					,	@EntityKey				= @NotificationSubscriberId
					,	@AuditAction			= 'Clone'
					,	@CreatedDate			= @AuditDate
					,	@CreatedByPersonId		= @AuditId	

			END	
			GO
