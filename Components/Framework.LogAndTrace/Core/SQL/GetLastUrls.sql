 SELECT		UserLoginHistoryId                         
        ,	ApplicationId
		,	UserName                               
        ,	UserId                                     
        ,	URL                                        
        ,	ServerName	                               
        ,	DateVisited                                
FROM		dbo.UserLoginHistory                       
WHERE		UserLoginHistoryId                               
IN			(                                                   
				SELECT 
				TOP	@numOfRecords@                     
							MAX(UserLoginHistoryId) AS UserLoginHistoryId   
				FROM		dbo.UserLoginHistory                           
				WHERE		UserId = @UserId@                  
				GROUP BY	URL                                    
				ORDER BY	MAX(DateVisited) DESC                              
			)                        